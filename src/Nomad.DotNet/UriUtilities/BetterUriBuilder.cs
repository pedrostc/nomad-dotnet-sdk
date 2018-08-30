using System;
using System.Linq;
using System.Collections.Generic;

namespace Nomad.DotNet.UriUtilities
{
    public class BetterUriBuilder
    {
        private const string ROOT_PATH = "/";
        private QueryStringBuilder queryBuilder { get; set; }
        private List<string> pathParts { get; set; }
        private UriBuilder uriBuilder { get; set; }

        public Uri Uri
        {
            get
            {
                uriBuilder.Path = Path;
                uriBuilder.Query = Query;
                return uriBuilder.Uri;
            }
        }
        public string Path
        {
            get
            {
                return pathParts.Count > 0 ?
                    pathParts
                    .Aggregate((path, part) => $"{path??"/"}/{part}") :
                    ROOT_PATH;
            }
        }
        public string Query
        {
            get
            {
                return queryBuilder.QueryString;
            }
        }

        public BetterUriBuilder()
        {
            uriBuilder = new UriBuilder();
            queryBuilder = new QueryStringBuilder();
            pathParts = new List<string>();
        }
        public BetterUriBuilder(string uri) : this(new Uri(uri))
        { }
        public BetterUriBuilder(Uri uri) : this()
        {
            uriBuilder = new UriBuilder(uri);
            initializePath();
            initializeQuery();
        }
        public BetterUriBuilder(string scheme, string hostName) : this()
        {
            uriBuilder = new UriBuilder(scheme, hostName);
        }
        public BetterUriBuilder(string scheme, string hostName, int portNumber) : this()
        {
            uriBuilder = new UriBuilder(scheme, hostName, portNumber);
        }

        private bool isRootPath(string path)
        {
            return path == ROOT_PATH;
        }
        private void initializePath()
        {
            string path = uriBuilder.Path;

            if (isRootPath(path)) return;

            string[] sourceParts = path.Split('/');
            sourceParts.ToList().ForEach(part => AddPathPart(part));
        }
        private void updateBuilderPath()
        {
            uriBuilder.Path = Path;
        }
        public void AddPathPart(string part)
        {
            pathParts.Add(part);
            updateBuilderPath();
        }
        public bool ContainsPathPart(string part)
        {
            return pathParts.Contains(part);
        }
        public void RemovePathPart(string part)
        {
            pathParts.Remove(part);
            updateBuilderPath();
        }
        public void ClearPath()
        {
            pathParts.Clear();
            updateBuilderPath();
        }

        private void initializeQuery()
        {

        }
        private void updateBuilderQuery()
        {
            uriBuilder.Query = Query;
        }
        public void AddQueryField(string name, string value)
        {
            queryBuilder.AddField(name, value);
            updateBuilderQuery();
        }
        public bool ContainsQueryField(string name)
        {
            return queryBuilder.ContainsField(name);
        }
        public void RemoveQueryField(string name)
        {
            queryBuilder.RemoveField(name);
            updateBuilderQuery();
        }
        public void ClearQuery()
        {
            queryBuilder.Clear();
            updateBuilderQuery();
        }
    }
}
