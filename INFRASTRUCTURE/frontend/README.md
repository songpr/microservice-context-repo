# Frontend Infrastructure

Infrastructure for the CX/Frontend layer including web hosting, CDN, and static assets.

## AWS Resources

### CloudFront Distribution
- **Purpose**: Global CDN for web applications
- **Configuration**: Edge locations, caching policies, SSL/TLS
- **File**: `aws/cloudfront.tf`

### S3 Static Website Hosting
- **Purpose**: Static website hosting for React/Vue/Angular apps
- **Configuration**: Public access, versioning, lifecycle policies
- **File**: `aws/s3-website.tf`

### AWS Amplify
- **Purpose**: Full-stack web application hosting
- **Configuration**: Build settings, custom domains, CI/CD
- **File**: `aws/amplify.tf`

## Azure Resources

### Azure Static Web Apps
- **Purpose**: Static web app hosting with API support
- **Configuration**: Build configuration, custom domains, authentication
- **File**: `azure/static-web-apps.bicep`

### Azure CDN
- **Purpose**: Global content delivery network
- **Configuration**: Endpoints, caching rules, SSL certificates
- **File**: `azure/cdn.bicep`

### Azure App Service (Frontend)
- **Purpose**: Web app hosting for server-side rendered applications
- **Configuration**: App service plan, deployment slots, scaling
- **File**: `azure/app-service-frontend.bicep`

## Security

- **SSL/TLS**: Enforce HTTPS for all web traffic
- **WAF**: Web Application Firewall protection
- **Authentication**: Integration with identity providers
- **CORS**: Cross-origin resource sharing configuration

## Monitoring

- **Performance**: Web application performance monitoring
- **Availability**: Uptime monitoring and alerting
- **Analytics**: User behavior and traffic analytics
- **Logs**: Access logs and error tracking
