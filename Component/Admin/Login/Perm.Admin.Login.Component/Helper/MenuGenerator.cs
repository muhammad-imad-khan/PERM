using Perm.Admin.Login.Component.APIModel;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Model.Setup;

namespace Perm.Admin.Login.Component.Helper;

public class MenuGenerator
{
    private readonly PermDataContext _permDataContext;

    public MenuGenerator(PermDataContext cubeDataContext)
    {
        _permDataContext = cubeDataContext;
    }

    public List<ResMenuModel> Generate(long userID)
    {
        //Get only allowed menus
        List<MenuGetResultModel> menuGetResultModels = _permDataContext.StoredProcedure<MenuGetResultModel>("Admin.Menu_Get", new { userID });

        //Get all menus to create parent-child relationship
        List<MenuModel> dbMenus = _permDataContext.Menu.ToList();

        //Get parents of menus
        SetParentMenuItem(menuGetResultModels, dbMenus);

        List<ResMenuModel> resMenuModels = new();

        //Create relationship between menu and page option and remove duplicate menu items that are returned from stored procedure due to multiple options
        LinkMenuWithPageOption(menuGetResultModels, resMenuModels);

        //Get only parent menus
        List<ResMenuModel> parentMenus = resMenuModels.Where(menu => menu.ParentMenuID == null).ToList();

        GenerateRecursiveMenu(parentMenus, resMenuModels);

        parentMenus = parentMenus.OrderBy(m => m.SortNo).ThenBy(m => m.MenuID).ToList();
        return parentMenus;
    }

    private static void SetParentMenuItem(List<MenuGetResultModel> menuGetResultModels, List<MenuModel> dbMenus)
    {
        List<long?> parentMenuIds = menuGetResultModels.Select(s => s.ParentMenuID).Distinct().ToList();
        foreach (long? parentMenuID in parentMenuIds)
        {
            if (parentMenuID == null)
                continue;

            //If patent menu already exist/added to list
            if (menuGetResultModels.Exists(m => m.MenuID == parentMenuID.Value))
                continue;

            MenuModel menuModel = dbMenus.FirstOrDefault(m => m.MenuID == parentMenuID.Value);
            if (menuModel == null)
                continue;

            menuGetResultModels.Add(new MenuGetResultModel
            {
                MenuID = menuModel.MenuID,
                Link = menuModel.Link,
                Name = menuModel.Name,
                ParentMenuID = menuModel.ParentMenuID,
                SortNo = menuModel.SortNo,
            });

            //If newly added menu has a parent then check its parent menu recursively
            if (menuModel.ParentMenuID != null)
            {
                SetParentMenuItem(menuGetResultModels, dbMenus);
            }
        }
    }

    private static void LinkMenuWithPageOption(List<MenuGetResultModel> menuGetResultModels, List<ResMenuModel> resMenuModels)
    {
        foreach (MenuGetResultModel menuGetResultModel in menuGetResultModels)
        {
            ResMenuModel permissionMenuModel = resMenuModels.FirstOrDefault(m => m.MenuID == menuGetResultModel.MenuID);

            if (permissionMenuModel == null)
            {
                permissionMenuModel = new ResMenuModel
                {
                    MenuID = menuGetResultModel.MenuID,
                    Link = menuGetResultModel.Link,
                    Name = menuGetResultModel.Name,
                    ParentMenuID = menuGetResultModel.ParentMenuID,
                    SortNo = menuGetResultModel.SortNo
                };
                resMenuModels.Add(permissionMenuModel);
            }

            permissionMenuModel.PageOption ??= new List<ResPageOptionModel>();

            //Check if option name field has value, it will be null in case of parent menu.
            if (!string.IsNullOrEmpty(menuGetResultModel.OptionName))
            {
                permissionMenuModel.PageOption.Add(new ResPageOptionModel
                {
                    DisplayOrder = menuGetResultModel.DisplayOrder,
                    IsStandard = menuGetResultModel.IsStandardOption,
                    Key = menuGetResultModel.OptionKey,
                    Name = menuGetResultModel.OptionName,
                    PlacementArea = menuGetResultModel.PlacementArea
                });
            }
        }
    }

    private void GenerateRecursiveMenu(List<ResMenuModel> parentMenuList, List<ResMenuModel> flatMenuList)
    {
        foreach (ResMenuModel parentMenu in parentMenuList)
        {
            List<ResMenuModel> subMenuList = flatMenuList.Where(flatMenu => flatMenu.ParentMenuID == parentMenu.MenuID).ToList();

            if (subMenuList.Count != 0)
            {
                GenerateRecursiveMenu(subMenuList, flatMenuList);
            }

            parentMenu.SubMenu = subMenuList.Count == 0 ? null : subMenuList;

        }
    }

    private class MenuGetResultModel : MenuModel
    {
        public string OptionName { get; set; }
        public string OptionKey { get; set; }
        public string PlacementArea { get; set; }
        public bool IsStandardOption { get; set; }
        public int DisplayOrder { get; set; }
    }
}