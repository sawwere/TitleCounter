using hltb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class State<T> : ModeState where T : Content
    {
        private EFGenericRepository<T> repository = new EFGenericRepository<T>(new TitleCounterContext());

        public State(Mainform form) : base(form) { }


        public override void Create(Content content)
        {
            if (!(content is T))
                throw new InvalidCastException($"Cant cast content to {typeof(T)}");
            else
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                repository.Create(content as T);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }
        }

        public override void Load()
        {
            contents = repository.Get().Select(x => x as Content).ToList();
        }

        public override void Save()
        {
            using (TitleCounterContext db = new TitleCounterContext())
            {
                db.SaveChanges();
            }
        }

        public override void Update(Content content)
        {
            if (!(content is T))
                throw new InvalidCastException("Cant cast content to T");
            else
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                repository.Update(content as T);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }

        }

        public override void Remove(long id)
        {
            repository.Remove(repository.FindById(id));
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<T>(json_string);
        }

        public override string ToString()
        {
            return typeof(T).ToString().Split('.').Last();
        }
    }
}
