﻿namespace Web.Api.Infraestrutura.Common
{
    public class AnswerPaginate<T> : Answer<T>
    {
        public long TotalCount { get; set; }
        public string SkipToken { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}
