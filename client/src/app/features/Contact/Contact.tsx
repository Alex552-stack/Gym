// src/ContactUs.js

import {
  Container,
  Typography,
  Box,
  Grid,
  IconButton,
  Paper,
} from "@mui/material";
import { Email, LocationOn } from "@mui/icons-material";
import InstagramIcon from "@mui/icons-material/Instagram";
import FacebookIcon from "@mui/icons-material/Facebook";

const ContactUs = () => {
  return (
    <Container sx={{ mt: 5 }}>
      <Grid container spacing={3} alignItems="center" height="100%">
        <Grid item xs={12} md={6} height="100%">
          <Box
            display="flex"
            justifyContent="center"
            alignItems="center"
            height="500px"
            style={{ position: "relative" }}
          >
            <iframe
              src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d691.1056362506899!2d24.37586833320029!3d45.103680401953554!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x474d38a3925d7591%3A0x6c98e6bac927e125!2sVeterinary%20clinic%20Draghici!5e0!3m2!1sen!2sus!4v1716740816679!5m2!1sen!2sus"
              style={{
                position: "absolute",
                top: 0,
                left: 0,
                width: "100%",
                height: "100%",
              }}
            />
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
                <IconButton
                  href="https://www.instagram.com/yourprofile"
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  <InstagramIcon fontSize="large" />
                </IconButton>
                <Typography variant="body1">Instagram</Typography>
              </Grid>
              <Grid item>
                <IconButton
                  href="https://www.facebook.com/yourpage"
                  target="_blank"
                  rel="noopener noreferrer"
                >
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
