using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace SmartPriceTest
{
    class ProductAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly JavaList<Product> products;
        private LayoutInflater inflater;

        public ProductAdapter(Context c, JavaList<Product> products)
        {
            this.context = c;
            this.products = products;
        }

        public override int Count
        {
            get { return products.Size(); }
        }

        public override Object GetItem(int position)
        {
            return products.Get(position);
        }

        public override long GetItemId(int position)
        {
            return position;
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
                NameTxt = { Text = products[position].Name }
            };

            holder.Image.SetImageResource(products[position].Image);

            return convertView;
        }
    }

    class ProductAdapterViewHolder : Object
    {
        public TextView NameTxt;
        public ImageView Image;

        public ProductAdapterViewHolder(View itemView)
        {
            NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
            Image = itemView.FindViewById<ImageView>(Resource.Id.productImg);
        }
    }
}