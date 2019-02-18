using Android.App;
using Android.Views;
using Android.Widget;
using ShopPrep.Common.Models;
using ShopPrep.UIClassic.Android.Helpers;
using System.Collections.Generic;

namespace ShopPrep.UIClassic.Android.Adapters
{
    public class ProductsListAdapter : BaseAdapter<Product>
    {
        private readonly List<Product> items;
        private readonly Activity context;

        public ProductsListAdapter(Activity context, List<Product> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Product this[int position] => items[position];

        public override int Count => items.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(item.ImageFullPath);

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ProductRow, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.nameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = $"{item.Price:C2}";
            convertView.FindViewById<ImageView>(Resource.Id.productImageView).SetImageBitmap(imageBitmap);

            return convertView;
        }
    }
}