# INR - Interactive Neurorehabilitation Rating System

## Overview

INR (Interactive Neurorehabilitation Rating System) is a .NET 6 Web API backend application that serves as the backend infrastructure for the [Research Segmentation Tool](https://github.com/zhaoyilucyguo/segmentation-interface). This system provides comprehensive data management, video processing, and rating capabilities for medical therapy video segmentation and analysis workflows.

The backend supports researchers and therapists in analyzing patient rehabilitation videos through advanced segmentation techniques, enabling precise therapeutic assessment and data collection for neurorehabilitation research and clinical practice.

## Features

### Core Functionality
- **Research Data Management**: Comprehensive backend support for segmentation research workflows
- **Patient & Task Management**: Track patients and their rehabilitation tasks with detailed metadata
- **Video Processing & Storage**: Handle rehabilitation video files with efficient storage and retrieval
- **Advanced Segmentation Support**: Backend infrastructure for complex image/video segmentation tasks
- **Rating & Assessment System**: Enable therapists and researchers to rate patient performance on tasks and segments
- **Feedback Collection**: Capture detailed researcher and therapist feedback on patient performance
- **Real-time Data Support**: Provide data services for real-time visualization and interactive feedback in the frontend
- **Research Analytics**: Generate comprehensive reports and analytics for research purposes

### Key Components
- **Patient Task Hand Mapping**: Maps patients to specific rehabilitation tasks and affected hands
- **Advanced Segmentation Backend**: Provides data services for complex video segmentation operations
- **Multi-Camera Support**: Handle recordings from multiple camera angles
- **Research Rating Interface**: Comprehensive rating system for task evaluation and data collection
- **Real-time Data Services**: Support frontend real-time visualization and interactive feedback
- **Database-Driven Architecture**: Full Entity Framework Core implementation with migrations

## System Architecture

This backend API works in conjunction with the [Research Segmentation Tool Frontend](https://github.com/zhaoyilucyguo/segmentation-interface) to provide a complete research platform.

**Technology Stack:**
- **Framework**: .NET 6 Web API
- **Database**: Microsoft SQL Server with Entity Framework Core
- **Architecture**: Repository Pattern with Unit of Work
- **Containerization**: Docker & Docker Compose
- **API Documentation**: Swagger/OpenAPI

## Project Structure

```
INR/
├── Controllers/                    # API Controllers
│   ├── RatingController.cs        # Rating and feedback endpoints
│   ├── SegmentationController.cs  # Video segmentation endpoints
│   ├── ReportingController.cs     # Reporting endpoints
│   └── ...
├── DAL/                           # Data Access Layer
│   ├── Models/                    # Entity models
│   │   ├── Patient.cs
│   │   ├── PatientTaskHandMapping.cs
│   │   ├── TaskRating.cs
│   │   ├── SegmentRating.cs
│   │   ├── VideoSegment.cs
│   │   └── ...
│   ├── Repositories/              # Repository implementations
│   ├── InrDbContext.cs           # Database context
│   └── UnitOfWork.cs             # Unit of work pattern
├── Services/                      # Business logic services
├── Migrations/                    # EF Core migrations
└── Models/                       # Request/Response models
```

## Database Schema

Key entities managed by the system:

- **Patient**: Individual patients in the rehabilitation program
- **PatientTaskHandMapping**: Maps patients to specific tasks and affected hands
- **TaskRating**: Overall ratings for rehabilitation tasks
- **SegmentRating**: Detailed ratings for video segments
- **VideoSegment**: Video segment definitions with timing information
- **Feedback**: Therapist feedback and comments

## API Endpoints

### Rating Controller (`/api/Rating/`)
- `GET GetPthByTherapistId/{therapistId}` - Get patient-task mappings for a therapist
- `GET GetSegmentFileInfo/{pthId}` - Get segment file information
- `GET GetAlreadySubmittedRating/{pthId}/{therapistId}` - Retrieve existing ratings
- `POST SubmitRating` - Submit task and segment ratings
- `POST SubmitFeedback` - Submit therapist feedback
- `GET GetSubmittedFeedbacks` - Query submitted feedback

### Segmentation Controller (`/api/Segmentation/`)
- `GET GetUnImapiredVideo` - Get unimpaired hand videos for comparison
- `GET GetPatientTaskInformation` - Get patient task mappings
- `GET GetFiles/{pthId}` - Get video files for a patient task

## Setup and Installation

### Prerequisites
- .NET 6 SDK
- Docker and Docker Compose
- SQL Server (or use Docker container)

### Local Development Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/sadidkhan/INR.git
   cd INR
   ```

2. **Using Docker Compose (Recommended):**
   ```bash
   docker-compose up -d
   ```

3. **Manual Setup:**
   ```bash
   # Restore NuGet packages
   dotnet restore

   # Update database connection string in appsettings.json
   # Run database migrations
   dotnet ef database update

   # Run the application
   dotnet run
   ```

### Docker Deployment

The application includes Docker support with SQL Server 2019 container and automated database migration on startup.

### Database Configuration

**Connection String Format:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=inr-primary;User Id=sa;Password=your_password;"
  }
}
```

## Configuration

- `FileDirectory`: Path to video file storage
- `ConnectionStrings`: Database connection configuration
- `SA_PASSWORD`: SQL Server SA password (Docker)
- `ACCEPT_EULA`: SQL Server EULA acceptance (Docker)

## Usage

### API Documentation
When running in development mode, Swagger UI is available at:
```
https://localhost:5001/swagger
```

### Key Workflows

1. **Research Setup**: Configure patients, tasks, and research parameters
2. **Video Processing**: Upload rehabilitation videos and prepare for segmentation
3. **Advanced Segmentation**: Support frontend segmentation tools with real-time data services
4. **Rating & Assessment**: Collect researcher and therapist ratings on patient performance
5. **Analytics**: Generate comprehensive reports and export research-grade data

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## Dependencies

- `Microsoft.EntityFrameworkCore.SqlServer` (6.0.10)
- `Microsoft.EntityFrameworkCore.Tools` (6.0.10)
- `Swashbuckle.AspNetCore` (6.2.3)

## Related Projects

**Frontend Interface**: [Research Segmentation Tool](https://github.com/zhaoyilucyguo/segmentation-interface) - React-based frontend with advanced segmentation capabilities, real-time visualization, and interactive feedback designed for researchers.

The INR backend and segmentation frontend work together to provide seamless data flow, real-time synchronization, and high-quality research data outputs.

## License

This project is currently private. Please contact the repository owner for licensing information.

## Support

For questions or support, please open an issue in the GitHub repository or contact the development team.

---

**Note**: This system is designed for research and clinical use in neurorehabilitation. Ensure compliance with relevant healthcare data protection regulations when deploying in clinical environments.
