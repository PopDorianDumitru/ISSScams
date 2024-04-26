using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha
{
    internal class MockPost : IKeyedEntity<int>
    {
        public override bool Equals(object? comparisonObject)
        {
            if (comparisonObject == null || !GetType().Equals(comparisonObject.GetType()))
            {
                return false;
            }
            else
            {
                MockPost post = (MockPost)comparisonObject;
                return (Id == post.Id) && (PosterId == post.PosterId) && (PostTitle == post.PostTitle) && (PostContent == post.PostContent) && (PostDate == post.PostDate);
            }
        }

        private int id;
        public int Id
        {
            get { return id; } set { id = value; }
        }

        private int poster_id;
        public int PosterId
        {
            get { return poster_id; } set { poster_id = value; }
        }

        private string post_title;
        public string PostTitle
        {
            get { return post_title; } set { post_title = value; }
        }

        private string post_content;
        public string PostContent
        {
            get { return post_content; } set { post_content = value; }
        }

        private DateTime post_date;
        public DateTime PostDate
        {
            get { return post_date; } set { post_date = value; }
        }

        public MockPost(int id, int user_id, string post_title, string post_content, DateTime post_date)
        {
            this.id = id;
            poster_id = user_id;
            this.post_title = post_title;
            this.post_content = post_content;
            this.post_date = post_date;
        }

        public int GetId()
        {
            return id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{id} - {poster_id} - {post_title} - {post_content} - {post_date}";
        }
    }
}
