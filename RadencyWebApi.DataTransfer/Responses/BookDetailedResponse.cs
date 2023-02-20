﻿namespace RadencyWebApi.DataTransfer.Responses;

public class BookDetailedResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
    public List<ReviewResponse> Reviews { get; set; }
}