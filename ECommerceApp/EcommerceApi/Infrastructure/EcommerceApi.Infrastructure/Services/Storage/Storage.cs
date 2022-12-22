using Ecommerce.Infrastructure.Operations;

namespace Ecommerce.Infrastructure.Services.Storage;

public class Storage
{
    protected delegate bool HasFile(string source, string fileName);

    protected async Task<string> FileRenameAsync(string source, string fileName, HasFile hasFileMethod, bool first = true)
    {
        string newFileName = await Task.Run(async () =>
        {
            string extension = Path.GetExtension(fileName);
            string newFileName;
            if (first)
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                newFileName = fileName;
                int indexNo1 = newFileName.IndexOf("-", StringComparison.Ordinal);
                if (indexNo1 == -1)
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                else
                {
                    while (true)
                    {
                        var lastIndex = indexNo1;
                        indexNo1 = newFileName.IndexOf("-", indexNo1 + 1, StringComparison.Ordinal);
                        if (indexNo1 != -1) continue;
                        indexNo1 = lastIndex;
                        break;
                    }

                    int indexNo2 = newFileName.IndexOf(".", StringComparison.Ordinal);
                    string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                    if (int.TryParse(fileNo, out int fileHelper))
                    {
                        fileHelper++;
                        newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                            .Insert(indexNo1 + 1, fileHelper.ToString());
                    }
                    else
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                }
            }

            if (hasFileMethod(source, newFileName))
                return await FileRenameAsync(source, newFileName, hasFileMethod, false);
            else
                return newFileName;
        });

        return newFileName;
    }
}