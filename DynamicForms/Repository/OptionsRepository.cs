using DynamicForms.Data;
using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using DynamicForms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicForms.Repository;

public class OptionsRepository(DbContextOptions<AppDbContext> options):IPostSender<List<OptionsDTO>>
{

    protected readonly DbContextOptions<AppDbContext> _dbOptions = options;
    public async Task<MessageInfoDTO> PostAsync(List<OptionsDTO> dtos)
    {
        try
        {
            using var context = new AppDbContext(_dbOptions);
            List<FormTypeData> formTypesData = [
                new(){
                Name = "Date",
            },
            new(){
                Name = "Number",
            },
            new(){
                Name = "Text",
            },
        ];
            List<Form> forms = [
                new(){
                OptionId = dtos[0].OptionId,
                Name = "Personas",
                IsActive = true,
                Inputs = [
                    new(){
                        Label = "Nombres",
                        FormTypeData = formTypesData[2],
                        IsActive = true,
                    },
                    new(){
                        Label = "Fecha de nacimiento",
                        FormTypeData = formTypesData[0],
                        IsActive = true,
                    },
                    new(){
                        Label = "Estatura",
                        FormTypeData = formTypesData[1],
                        IsActive = true,
                    },
                ],
            },
            new(){
                Name = "Mascotas",
                IsActive = true,
                OptionId = dtos[1].OptionId,
                Inputs = [
                    new(){
                        Label = "Nombre",
                        FormTypeData = formTypesData[2],
                        IsActive = true,
                    },
                    new(){
                        Label = "Color",
                        FormTypeData = formTypesData[2],
                        IsActive = true,
                    },
                    new(){
                        Label = "Raza",
                        FormTypeData = formTypesData[2],
                        IsActive = true,
                    },
                    new(){
                        Label = "Especie",
                        FormTypeData = formTypesData[2],
                        IsActive = true,
                    },
                ],
            }
            ];
            if (!context.FormTypeData.Any() && !context.Forms.Any())
            {
                await context.AddRangeAsync(forms);
                await context.SaveChangesAsync();
            }
                return new MessageInfoDTO
                {
                    Message = "Carga inicial hecha",
                };
        }
        catch (Exception ex)
        {

            return new MessageInfoDTO
            {
                Message = ex.Message,
            };
        }
    }
}
