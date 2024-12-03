namespace Client.DataFetch
{
    internal interface IFetchData
    {
        Task<(string Type, string Data)> Fetch();
    }
}