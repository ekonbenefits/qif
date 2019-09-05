namespace QifApi.Transactions
{
    public class TagListTransaction : TransactionBase
    {
        public string TagName { get; set; }

        public string Description { get; set; }

        public TagListTransaction()
        {
            this.TagName = "";
            this.Description = "";
        }

        public override string ToString()
        {
            return this.TagName;
        }
    }
}