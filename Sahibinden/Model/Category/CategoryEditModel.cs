﻿namespace Sahibinden.Model.Category
{
    public class CategoryEditModel
    {

        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }

    }
}