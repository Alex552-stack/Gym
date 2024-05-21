// src/ContactUs.js

import { Container, Typography, Box, Grid, IconButton, Paper } from '@mui/material';
import { Email, LocationOn } from '@mui/icons-material';
import InstagramIcon from '@mui/icons-material/Instagram';
import FacebookIcon from '@mui/icons-material/Facebook';
import { GoogleMap, LoadScript, Marker } from '@react-google-maps/api';

const mapContainerStyle = {
  width: '100%',
  height: '400px',
  borderRadius: '8px',
};

const center = {
  lat: 45.1047, // Latitude of Râmnicu Vâlcea
  lng: 24.3754, // Longitude of Râmnicu Vâlcea
};

const ContactUs = () => {
  return (
    <Container sx={{ mt: 5 }}>
      <Grid container spacing={3} alignItems="center">
        <Grid item xs={12} md={6}>
          <Box display="flex" justifyContent="center" alignItems="center" height="100%">
            <LoadScript googleMapsApiKey="AIzaSyAqKQzS_NPGKfEKeylE-7AQc3n_QbMo4l8">
              <GoogleMap
                mapContainerStyle={mapContainerStyle}
                center={center}
                zoom={15}
              >
                <Marker position={center} />
              </GoogleMap>
            </LoadScript>
          </Box>
        </Grid>
        <Grid item xs={12} md={6}>
          <Box
            component={Paper}
            elevation={3}
            bgcolor="#69696"
            p={5}
            textAlign="center"
          >
            <Typography variant="h4" gutterBottom>
              Contact Us
            </Typography>
            <Grid container spacing={3} direction="column" alignItems="center">
              <Grid item>
                <IconButton href="https://www.instagram.com/yourprofile" target="_blank" rel="noopener noreferrer">
                  <InstagramIcon fontSize="large" />
                </IconButton>
                <Typography variant="body1">Instagram</Typography>
              </Grid>
              <Grid item>
                <IconButton href="https://www.facebook.com/yourpage" target="_blank" rel="noopener noreferrer">
                  <FacebookIcon fontSize="large" />
                </IconButton>
                <Typography variant="body1">Facebook</Typography>
              </Grid>
              <Grid item>
                <IconButton href="mailto:youremail@example.com">
                  <Email fontSize="large" />
                </IconButton>
                <Typography variant="body1">Email</Typography>
              </Grid>
              <Grid item>
                <IconButton disabled>
                  <LocationOn fontSize="large" />
                </IconButton>
                <Typography variant="body1">
                  123 Your Street, Your City, Your Country
                </Typography>
              </Grid>
            </Grid>
          </Box>
        </Grid>
      </Grid>
    </Container>
  );
};

export default ContactUs;
