import { Box, Container, Grid, Typography } from '@mui/material';
import AboutImage from '../../../Images/gymowner2 (2).png'; 

export default function AboutPage() {
  return (
    <Container maxWidth="lg">
      <Box sx={{ mt: 8, mb: 8 }}>
        <Typography variant="h2" component="h1" gutterBottom sx={{ textAlign: 'center', color: '#fff' }}>
          About Us
        </Typography>
        <Typography variant="h5" component="p" gutterBottom sx={{ textAlign: 'center', color: '#fff', mb: 4 }}>
          We are dedicated to helping you achieve your fitness goals.
        </Typography>
        <Grid container spacing={4} alignItems="center">
          <Grid item xs={12} md={6}>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
              <img 
                src={AboutImage} 
                alt="About Us" 
                style={{ 
                  width: '100%', 
                  height: 'auto', 
                  maxWidth: '100%'  
                }} 
              />
            </Box>
          </Grid>
          <Grid item xs={12} md={6}>
            <Box sx={{ backgroundColor: '#696969', padding: '20px', borderRadius: '10px' }}>
              <Typography variant="h6" component="h2" gutterBottom sx={{ color: '#fff', fontWeight: 'bold' }}>
                Our Mission
              </Typography>
              <Typography variant="body1" paragraph sx={{ color: '#fff' }}>
                Our mission is to provide you with the best tools and resources to help you stay fit and healthy. We believe in creating a supportive community where everyone can thrive and achieve their personal fitness goals.
              </Typography>
              <Typography variant="h6" component="h2" gutterBottom sx={{ color: '#fff', fontWeight: 'bold' }}>
                Our Team
              </Typography>
              <Typography variant="body1" paragraph sx={{ color: '#fff' }}>
                Our team is composed of experienced fitness professionals who are passionate about helping others. From certified personal trainers to nutrition experts, we are here to guide you every step of the way.
              </Typography>
              <Typography variant="h6" component="h2" gutterBottom sx={{ color: '#fff', fontWeight: 'bold' }}>
                Join Us
              </Typography>
              <Typography variant="body1" paragraph sx={{ color: '#fff' }}>
                Join us on this exciting journey to better health and wellness. Whether you are just starting out or looking to take your fitness to the next level, we have something for everyone.
              </Typography>
            </Box>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
}
