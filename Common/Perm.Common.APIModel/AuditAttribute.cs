namespace Perm.Common.APIModel;

[AttributeUsage(AttributeTargets.Property)]
public class AuditAttribute : Attribute
{
    public bool Ignore { get; set; }
    public string DisplayName { get; set; }
}