﻿namespace PawfectMatch.Components.Pages.Models
{
    public class FormResult
    {
        public bool Succeeded { get; set; }
        public string[] ErrorList { get; set; } = [];
    }
}