using hltb.Models;

namespace hltb.Forms.ContentListBuilder
{
    public interface IFilterContentStrategy
    {
        IEnumerable<Content> Filter(List<Content> contents, string arg);
    }

    public class FilterByYear : IFilterContentStrategy
    {
        public IEnumerable<Content> Filter(List<Content> contents, string arg)
        {
            return contents.Where(x => x.DateRelease.Year.ToString() == arg);
        }
    }

    public class FilterByScore : IFilterContentStrategy
    {
        public IEnumerable<Content> Filter(List<Content> contents, string arg)
        {
            int value = int.Parse(arg);
            return contents.Where(x => x.Score == value);
        }
    }

    public class FilterByStatus : IFilterContentStrategy
    {
        public IEnumerable<Content> Filter(List<Content> contents, string arg)
        {
            arg = arg.ToLower();
            return contents.Where(x => x.Status.ToString() == arg);
        }
    }

    public class FilterByName : IFilterContentStrategy
    {
        public IEnumerable<Content> Filter(List<Content> contents, string arg)
        {
            return contents.Where(x => x.Title.StartsWith(arg));
        }
    }
}
