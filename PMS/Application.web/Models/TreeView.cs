using Remotion.Data.Linq.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class TreeView
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string name { get; set; }
        public ObservableCollection<TreeView> ChildTopics { get; set; }

        public TreeView()
        {
            ChildTopics = new ObservableCollection<TreeView>();
        }

        public TreeView(int idValue, int parentId = 0, string Name = "")
            : this()
        {
            id = idValue;
            parent_id = parentId;
            name = Name;
        }
    }
}