﻿using System;

namespace GoogleOAuthExample.Services
{
    [Serializable]
    public class MyAuthResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }
}