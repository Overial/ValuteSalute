using ValuteSalute.Domain.Repositories.Abstract;

namespace ValuteSalute.Domain
{
    // Helper class
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }

        public DataManager
        (
            ITextFieldsRepository textFields
        )
        {
            this.TextFields = textFields;
        }
    }
}
