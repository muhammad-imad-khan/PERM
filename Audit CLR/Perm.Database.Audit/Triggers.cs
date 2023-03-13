using System;
using Microsoft.SqlServer.Server;

namespace Perm.Database.Audit
{
    public partial class Triggers
    {
        public static void TriggerForAudit()
        {
            if (SqlContext.Pipe is null)
                return;
            try
            {
                SqlTriggerContext context = SqlContext.TriggerContext;
                TriggerHandlerBase triggerHandlerBase = null;
                if (context != null)
                {
                    switch (context.TriggerAction)
                    {
                        case TriggerAction.Insert:
                            triggerHandlerBase = new AddHandler();
                            break;
                        case TriggerAction.Update:
                            triggerHandlerBase = new UpdateHandler();
                            break;
                        case TriggerAction.Delete:
                            triggerHandlerBase = new DeleteHandler();
                            break;
                    }
                }

                triggerHandlerBase?.Handle();
            }
            catch (Exception ex)
            {
                string message = $"Message: {ex.Message}{Environment.NewLine} Stack Trace:{ex.StackTrace}";

                if (message.Length > 4000)
                {
                    message = message.Substring(0, 3999);
                }

                SqlContext.Pipe.Send(message.Substring(0, 3999));
                throw;
            }
        }
    }
}