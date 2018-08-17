using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System.Collections.Generic;
using System.IO;

namespace SmartPrice
{
    class ProductAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly List<PriceDTO> products;
        private LayoutInflater inflater;

        public ProductAdapter(Context c, List<PriceDTO> products)
        {
            this.context = c;
            this.products = products;
        }

        public override int Count
        {
            get { return products.Count; }
        }

        public override Object GetItem(int position)
        {
            throw new System.NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public PriceDTO GetItemById(int position)
        {
            return products[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null)
            {
                inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            }

            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.ProductRow, parent, false);
            }

            ProductAdapterViewHolder holder = new ProductAdapterViewHolder(convertView)
            {
                ValueTxt = { Text = products[position].FromCurrency + " -> " + products[position].ToCurrency + " : " + products[position].ExchangedValue },
                NameTxt = { Text = products[position].product.Name },
                DescriptionTxt = { Text = products[position].product.Description}
            };
            
            var pictures = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = System.IO.Path.Combine(pictures, "pic" + products[position].PicturePathId + ".png");

            try
            {
                var f = File.ReadAllBytes(filePath);
                Bitmap pic = BitmapFactory.DecodeStream(new MemoryStream(f));

                holder.Image.SetImageBitmap(pic);
            }
            catch(Exception ex)
            {

            }
            return convertView;
        }
    }

    class ProductAdapterViewHolder : Object
    {
        public TextView NameTxt;
        public TextView DescriptionTxt;
        public TextView ValueTxt;
        public ImageView Image;

        public ProductAdapterViewHolder(View itemView)
        {
            ValueTxt = itemView.FindViewById<TextView>(Resource.Id.valueTxt);
            NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
            DescriptionTxt = itemView.FindViewById<TextView>(Resource.Id.descriptionTxt);
            Image = itemView.FindViewById<ImageView>(Resource.Id.productImg);
        }
    }
}