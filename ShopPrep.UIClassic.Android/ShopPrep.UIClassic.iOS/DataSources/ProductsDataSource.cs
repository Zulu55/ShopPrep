namespace ShopPrep.UIClassic.iOS.DataSources
{
    using System;
    using System.Collections.Generic;
    using Common.Models;
    using Foundation;
    using UIKit;

    public class ProductsDataSource : UITableViewSource
    {
        private readonly List<Product> products;
        private readonly NSString cellIdentifier = new NSString("ProductCell");

        public ProductsDataSource(List<Product> products)
        {
            this.products = products;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as UITableViewCell;

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }

            var product = products[indexPath.Row];
            cell.TextLabel.Text = product.Name;
            cell.ImageView.Image = UIImage.FromFile(product.ImageFullPath);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this.products.Count;
        }
    }
}