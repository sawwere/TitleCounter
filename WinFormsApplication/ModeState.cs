using hltb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public abstract class ModeState
    {
        protected List<Content> contents;
        protected Mainform form;
        public List<Content> Contents
        { get { return contents; } }

        public ModeState(Mainform form)
        {
            contents = new List<Content>();
            this.form = form;
        }

        public abstract void Load();

        public abstract void Create(Content content);

        public abstract void Update(Content content);

        public abstract void Remove(long id);

        public abstract Content? GetFromJson(string json_string);

        new public abstract string ToString();
    }
}
