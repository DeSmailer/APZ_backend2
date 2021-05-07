using BusinessLogicLayer.Interfaces;
using ClosedXML.Excel;
using DataAccessLayer.Models.Entities;
using DataAccessLayer.Models.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ExportImportService : IExportImportService
    {
        private readonly IRepository repository;

        public ExportImportService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<byte[]> Export()
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {

                int currentRow = 1;


                //_____________________________________________________________________________________________________________________
                var workstheet1 = workbook.Worksheets.Add("Chat");
                currentRow = 1;
                workstheet1.Cell(currentRow, 1).Value = "Id";
                workstheet1.Cell(currentRow, 2).Value = "InitiatorId";
                workstheet1.Cell(currentRow, 3).Value = "RecipientId";
                workstheet1.Cell(currentRow, 4).Value = "InstitutionId";

                var tokens = await this.repository.GetRangeAsync<Chat>(true, x => true);

                foreach (var item in tokens)
                {
                    currentRow++;
                    workstheet1.Cell(currentRow, 1).Value = item.Id;
                    workstheet1.Cell(currentRow, 2).Value = item.InitiatorId;
                    workstheet1.Cell(currentRow, 3).Value = item.RecipientId;
                    workstheet1.Cell(currentRow, 4).Value = item.InstitutionId;
                }
                //_____________________________________________________________________________________________________________________


                var workstheet2 = workbook.Worksheets.Add("Institution");
                currentRow = 1;
                workstheet2.Cell(currentRow, 1).Value = "Id";
                workstheet2.Cell(currentRow, 2).Value = "Name";
                workstheet2.Cell(currentRow, 3).Value = "Location";

                var institution = await this.repository.GetRangeAsync<Institution>(true, x => true);

                foreach (var item in institution)
                {
                    currentRow++;
                    workstheet2.Cell(currentRow, 1).Value = item.Id;
                    workstheet2.Cell(currentRow, 2).Value = item.Name;
                    workstheet2.Cell(currentRow, 3).Value = item.Location;
                }
                //_____________________________________________________________________________________________________________________



                var workstheet3 = workbook.Worksheets.Add("InstitutionEmployee");
                currentRow = 1;
                workstheet3.Cell(currentRow, 1).Value = "Id";
                workstheet3.Cell(currentRow, 2).Value = "InstitutionId";
                workstheet3.Cell(currentRow, 3).Value = "UserId";
                workstheet3.Cell(currentRow, 4).Value = "Role";
                workstheet3.Cell(currentRow, 5).Value = "IsWorking";

                var institutionEmployee = await this.repository.GetRangeAsync<InstitutionEmployee>(true, x => true);

                foreach (var item in institutionEmployee)
                {
                    currentRow++;
                    workstheet3.Cell(currentRow, 1).Value = item.Id;
                    workstheet3.Cell(currentRow, 2).Value = item.InstitutionId;
                    workstheet3.Cell(currentRow, 3).Value = item.UserId;
                    workstheet3.Cell(currentRow, 4).Value = item.Role;
                    workstheet3.Cell(currentRow, 5).Value = item.IsWorking;
                }
                //_____________________________________________________________________________________________________________________

                var workstheet4 = workbook.Worksheets.Add("Message");
                currentRow = 1;
                workstheet4.Cell(currentRow, 1).Value = "Id";
                workstheet4.Cell(currentRow, 2).Value = "ChatId";
                workstheet4.Cell(currentRow, 3).Value = "SenderId";
                workstheet4.Cell(currentRow, 4).Value = "Text";
                workstheet4.Cell(currentRow, 5).Value = "Time";

                var message = await this.repository.GetRangeAsync<Message>(true, x => true);

                foreach (var item in message)
                {
                    currentRow++;
                    workstheet4.Cell(currentRow, 1).Value = item.Id;
                    workstheet4.Cell(currentRow, 2).Value = item.ChatId;
                    workstheet4.Cell(currentRow, 3).Value = item.SenderId;
                    workstheet4.Cell(currentRow, 4).Value = item.Text;
                    workstheet4.Cell(currentRow, 5).Value = item.Time.ToString();
                }
                //_____________________________________________________________________________________________________________________


                var workstheet5 = workbook.Worksheets.Add("PaymentHistory");
                currentRow = 1;
                workstheet5.Cell(currentRow, 1).Value = "Id";
                workstheet5.Cell(currentRow, 2).Value = "WalletId";
                workstheet5.Cell(currentRow, 3).Value = "Date";
                workstheet5.Cell(currentRow, 4).Value = "MoneyTransaction";
                workstheet5.Cell(currentRow, 5).Value = "Remainder";

                var paymentHistory = await this.repository.GetRangeAsync<PaymentHistory>(true, x => true);

                foreach (var item in paymentHistory)
                {
                    currentRow++;
                    workstheet5.Cell(currentRow, 1).Value = item.Id;
                    workstheet5.Cell(currentRow, 2).Value = item.WalletId;
                    workstheet5.Cell(currentRow, 3).Value = item.Date.ToString();
                    workstheet5.Cell(currentRow, 4).Value = item.MoneyTransaction;
                    workstheet5.Cell(currentRow, 5).Value = item.Remainder;
                }
                //_____________________________________________________________________________________________________________________



                var workstheet6 = workbook.Worksheets.Add("User");
                currentRow = 1;
                workstheet6.Cell(currentRow, 1).Value = "Id";
                workstheet6.Cell(currentRow, 2).Value = "Name";
                workstheet6.Cell(currentRow, 3).Value = "Surname";
                workstheet6.Cell(currentRow, 4).Value = "Email";
                workstheet6.Cell(currentRow, 5).Value = "Password";
                workstheet6.Cell(currentRow, 6).Value = "Feature";

                var users = await this.repository.GetRangeAsync<User>(true, x => true);

                foreach (var item in users)
                {
                    currentRow++;
                    workstheet6.Cell(currentRow, 1).Value = item.Id;
                    workstheet6.Cell(currentRow, 2).Value = item.Name;
                    workstheet6.Cell(currentRow, 3).Value = item.Surname;
                    workstheet6.Cell(currentRow, 4).Value = item.Email;
                    workstheet6.Cell(currentRow, 5).Value = item.Password;
                    workstheet6.Cell(currentRow, 6).Value = item.Feature;
                }
                //_____________________________________________________________________________________________________________________



                var workstheet7 = workbook.Worksheets.Add("Wallet");
                currentRow = 1;
                workstheet7.Cell(currentRow, 1).Value = "Id";
                workstheet7.Cell(currentRow, 2).Value = "InstitutionId";
                workstheet7.Cell(currentRow, 3).Value = "CurrentBalance";
                workstheet7.Cell(currentRow, 4).Value = "CostPerDay";

                var wallet = await this.repository.GetRangeAsync<Wallet>(true, x => true);

                foreach (var item in wallet)
                {
                    currentRow++;
                    workstheet7.Cell(currentRow, 1).Value = item.Id;
                    workstheet7.Cell(currentRow, 2).Value = item.InstitutionId;
                    workstheet7.Cell(currentRow, 3).Value = item.CurrentBalance;
                    workstheet7.Cell(currentRow, 4).Value = item.CostPerDay;
                }
                //_____________________________________________________________________________________________________________________


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }

            }
        }
    }
}
