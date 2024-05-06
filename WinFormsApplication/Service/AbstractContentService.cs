using hltb.Dto;
using hltb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Service
{
    public abstract class AbstractContentService
    {
        protected List<Content> contents;
        public List<Content> Contents
        { get { return contents; } }

        public AbstractContentService()
        {
            contents = new List<Content>();
        }

        public abstract IEnumerable<ISearchable> SearchByTitle(string title);

        public abstract void Load();

        public abstract void Create(ISearchable content);

        public abstract void Update(Content content);

        public abstract void Remove(long id);

        public abstract Content? GetFromJson(string json_string);

        new public abstract string ToString();
    }
}
