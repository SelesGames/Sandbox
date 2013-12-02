
namespace Sandbox.WebApp.ViewModels
{
    public class BoxContainerVM
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }




        #region override ToString method

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, ImageUrl);
        }

        #endregion
    }
}