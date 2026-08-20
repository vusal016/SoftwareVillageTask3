namespace StreamVibe.Domain.Entities
{
    public sealed class Faq : BaseEntity
    {
        private Faq()
        {
            
        }
        public Faq(string question, string answer, int orderNumber)
        {
            SetQuestion(question);
            SetAnswer(answer);
            SetOrderNumber(orderNumber);
        }
        public string Question { get;private set; }
        public string Answer { get; private set; }
        public int OrderNumber { get; private set; }
        
        private void SetQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentException("Question can't be empty");
            Question = question;
        }
        private void SetAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException("Answer can't be empty");
            Answer = answer;
        }
        private void SetOrderNumber(int orderNumber)
        {
            if (orderNumber < 0)
                throw new ArgumentException("Order number can't be negative");
            OrderNumber = orderNumber;
        }
    }
}
