﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Responses;
namespace Implem.Pleasanter.Libraries.General
{
    public static class Error
    {
        public enum Types
        {
            None,
            InvalidRequest,
            Authentication,
            PasswordNotChanged,
            UpdateConflicts,
            DeleteConflicts,
            HasNotPermission,
            IncorrectCurrentPassword,
            PermissionNotSelfChange,
            DefinitionNotFound,
            CantSetAtTopOfSite,
            NotFound,
            RequireMailAddresses,
            RequireColumn,
            ExternalMailAddress,
            BadMailAddress,
            MailAddressHasNotSet,
            BadFormat,
            FileNotFound,
            NotRequiredColumn,
            InvalidCsvData,
            FailedReadFile,
            CanNotDisabled,
            AlreadyAdded,
            InvalidFormula
        }

        public static bool Has(this Types type)
        {
            return type != Types.None;
        }

        public static Message Message(this Types type, params string[] data)
        {
            switch (type)
            {
                case Types.InvalidRequest:
                    return Messages.InvalidRequest(data);
                case Types.Authentication:
                    return Messages.Authentication(data);
                case Types.PasswordNotChanged:
                    return Messages.PasswordNotChanged(data);
                case Types.UpdateConflicts:
                    return Messages.UpdateConflicts(data);
                case Types.DeleteConflicts:
                    return Messages.DeleteConflicts(data);
                case Types.HasNotPermission:
                    return Messages.HasNotPermission(data);
                case Types.IncorrectCurrentPassword:
                    return Messages.IncorrectCurrentPassword(data);
                case Types.PermissionNotSelfChange:
                    return Messages.PermissionNotSelfChange(data);
                case Types.DefinitionNotFound:
                    return Messages.DefinitionNotFound(data);
                case Types.CantSetAtTopOfSite:
                    return Messages.CantSetAtTopOfSite(data);
                case Types.NotFound:
                    return Messages.NotFound(data);
                case Types.RequireMailAddresses:
                    return Messages.RequireMailAddresses(data);
                case Types.RequireColumn:
                    return Messages.RequireColumn(data);
                case Types.ExternalMailAddress:
                    return Messages.ExternalMailAddress(data);
                case Types.BadMailAddress:
                    return Messages.BadMailAddress(data);
                case Types.MailAddressHasNotSet:
                    return Messages.MailAddressHasNotSet(data);
                case Types.BadFormat:
                    return Messages.BadFormat(data);
                case Types.FileNotFound:
                    return Messages.FileNotFound(data);
                case Types.NotRequiredColumn:
                    return Messages.NotRequiredColumn(data);
                case Types.InvalidCsvData:
                    return Messages.InvalidCsvData(data);
                case Types.FailedReadFile:
                    return Messages.FailedReadFile(data);
                case Types.CanNotDisabled:
                    return Messages.CanNotDisabled(data);
                case Types.AlreadyAdded:
                    return Messages.AlreadyAdded(data);
                case Types.InvalidFormula:
                    return Messages.InvalidFormula(data);
                default: return null;
            }
        }

        public static string MessageJson(this Types type, params string[] data)
        {
            return new ResponseCollection().Message(type.Message(data)).ToJson();
        }
    }
}