namespace Mvc
{
      using System.Web;


    public class PublinterApplicationContext
    {
        private const string APP_CONTEXT = "PublinterApplicationContext";

        public string UserId { get; private set; }
        public string EmpresaId { get; private set; }

        public PublinterApplicationContext(string UserId, string EmpresaId)
        {
            this.UserId = UserId;
            this.EmpresaId = EmpresaId;
        }

        public static PublinterApplicationContext Current
        {
            get { return (PublinterApplicationContext)HttpContext.Current.Items[APP_CONTEXT]; }
            set { HttpContext.Current.Items[APP_CONTEXT] = value; }
        }
    }
}
