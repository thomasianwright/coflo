﻿namespace Coflo.Abstractions.Steps.Models;

public class PendingActivity
{
    public string Token { get; set; }
    public string ActivityName { get; set; }
    public object Parameters { get; set; }
    public DateTime TokenExpiry { get; set; }
}