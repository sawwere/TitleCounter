using System;
using System.Collections.Generic;

namespace hltb.Models;

public partial class Status
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
