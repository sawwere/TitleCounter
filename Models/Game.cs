using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hltb.Models;

public partial class Game : Content
{
    public string? Platform { get; set; }
}
