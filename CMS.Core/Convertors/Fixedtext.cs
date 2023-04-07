namespace CMS.Core.Convertors
{
    public static class Fixedtext
    {
        public static string FixEmail(this string email)
        {
            return email.Trim().ToLower();
        }
    }
}
