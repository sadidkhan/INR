using INR.DAL;
using INR.DAL.Repositories;
using INR.DAL.Repositories.Interfaces;
using INR.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<InrDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICameraRepository, CameraRepository>();
//builder.Services.AddScoped<ICameraViewTypeRepository, CameraViewTypeRepository>();
builder.Services.AddScoped<ISegmentRepository, SegmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IFileInformationRepository, FileInformationRepository>();
builder.Services.AddScoped<IPatientTaskHandMappingRepository, PatientTaskHandMappingRepository>();
builder.Services.AddScoped<ITaskSegmentHandCameraMappingRepository, TaskSegmentHandCameraMappingRepository>();
builder.Services.AddScoped<IVideoSegmentRepository, VideoSegmentRepository>();
builder.Services.AddScoped<IVideoSegmentationHistoryRepository, VideoSegmentationHistoryRepository>();

builder.Services.AddScoped<ISegmentFileInformationRepository, SegmentFileInformationRepository>();
builder.Services.AddScoped<IPthTherapistMappingRepository, PthTherapistMappingRepository >();
builder.Services.AddScoped<ITaskRatingRepository, TaskRatingRepository>();
builder.Services.AddScoped<ISegmentRatingRepository, SegmentRatingRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IFileProcessingService, FileProcessingService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<InrDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
