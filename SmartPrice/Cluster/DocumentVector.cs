using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPrice.Cluster
{
    public class DocumentVector
    {
        //Content represents the document(or any other object) to be clustered
        public string Content { get; set; }
        //represents the tf*idf of  each document
        public float[] VectorSpace { get; set; }
    }
}
