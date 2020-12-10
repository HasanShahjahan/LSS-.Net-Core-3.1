using LSS.HCM.Core.Common.Exceptions;
using System;

namespace LSS.HCM.Core.Common.Extensions
{
    public static class EnumExtentions
    {
        public static string ToCamelCase(this Enum enumData)
        {
            var text = enumData.ToString();
            return char.ToLowerInvariant(text[0]) + text.Substring(1);
        }

        public static string GetValue(this Enum emumData)
        {
            var value = Convert.ChangeType(emumData,
                emumData.GetTypeCode());
            return Convert.ToString(value);
        }


        public static string GetMessage(this ApplicationErrorCodes enumData)
        {
            switch (enumData)
            {
                case ApplicationErrorCodes.EmptyTransactionId: 
                    return "Specify Valid Transaction Id";
                default:
                    throw new ArgumentOutOfRangeException(nameof(enumData),enumData,null);
            }
        }
    }
}
