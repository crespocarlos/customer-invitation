namespace CustomerInvitation.Service.Interfaces
{
    public interface IInvitation
    {
        /// <summary>
        /// List the customers to be invited on console
        /// </summary>
        /// <param name="jsonFilePath">json file containing the customers data</param>
        /// <param name="maxDistance">max distance within office location which someone can be invited</param>
         void ListCustomersToInviteWithinDistance(string jsonFilePath, int maxDistance);
    }
}