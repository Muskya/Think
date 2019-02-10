using System;
using Microsoft.Xna.Framework.Content;

namespace Think
{
    class ContentHandler : ContentManager
    {
        public ContentHandler(IServiceProvider serviceProvider, string rootDirectory)
            :base(serviceProvider, rootDirectory)
        {

        }
    }
}
