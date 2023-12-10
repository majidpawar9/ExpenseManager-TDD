using System;

namespace ExpenseManagerLibrary
{
    public class SummaryWidget
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }

        public SummaryWidget(string type, string title, string amount)
        {
            Type = type;
            Title = title;
            Amount = amount;
        }

        public string Render()
        {
            return $@"
            <div class='col-md-4'>
                <div class='d-flex flex-row widget summary {Type}'>
                    <div class='d-flex flex-column justify-content-center p-5'>
                        <i class='fa-solid fa-dollar-sign fa-2xl'></i>
                    </div>
                    <div class='d-flex flex-column m-auto py-3'>
                        <span class='lead'>{Title}</span>
                        <h1 class='display-6 fw-bold'>{Amount}</h1>
                    </div>
                </div>
            </div>";
        }
    }
}
