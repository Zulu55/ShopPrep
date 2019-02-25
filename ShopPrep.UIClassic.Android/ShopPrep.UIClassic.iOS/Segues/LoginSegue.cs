using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ShopPrep.UIClassic.iOS.Segues
{
    [Register("LoginSegue")]
    public class LoginSegue : UIStoryboardSegue
    {
        public LoginSegue(IntPtr ptr) : base(ptr)
        {

        }

        public override void Perform()
        {
            //base.Perform();
            SourceViewController.NavigationController.PushViewController(ProductsViewController, false);

        }
    }
}