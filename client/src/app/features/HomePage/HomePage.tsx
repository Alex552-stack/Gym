import { Box, Grid, Typography } from '@mui/material';
import Image from '../../../Images/GymPictureGood.jpg'; 
import Image1 from '../../../Images/salaimagine.png';
import Image2 from '../../../Images/Picturenr1.png'  // Adjust the path if necessary
import { useState, useEffect } from 'react';

export default function HomePage() {
  const [isSmallScreen, setIsSmallScreen] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      setIsSmallScreen(window.innerWidth <= 768); // Adjust the breakpoint as needed
    };

    // Call handleResize initially
    handleResize();

    window.addEventListener('resize', handleResize);

    // Cleanup the event listener on unmount
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return (
    <div>
      <Grid
        container
        justifyContent="center"
        alignItems="center"
        sx={{
          height: '90vh', // Adjust for the height of the navbar
          width: '100%',
          margin: 0,
          padding: 0,
          backgroundImage: `url(${Image})`,
          backgroundSize: 'cover',
          backgroundRepeat: 'no-repeat'
        }}
      >
        <Grid item xs={12} sx={{ height: '100%', position: 'relative' }}>
          <Box style={{color:'pink', opacity: '100%'}}>
            <Typography
              variant="h3"
              component="div"
              sx={{
                position: 'absolute',
                top: '40%',
                left: '35%',
                transform: 'translate(-50%, -50%)',
                fontSize: isSmallScreen ? '80px' : '160px', 
                color: 'transparent',
                backgroundImage: 'linear-gradient(#fff, #fff)',
                backgroundRepeat: 'no-repeat',
                WebkitBackgroundClip: 'text',
                backgroundPosition: '-800px 0',
                animation: 'backcolor 3s linear infinite alternate',
                '@keyframes backcolor': {
                  '100%': {
                    backgroundPosition: '0 0',
                  },
                },
              }}
            >
               Get Fit
              With Us
            </Typography>
          </Box>
        </Grid>
        <Grid item xs={12} sx={{ textAlign: 'center', marginTop: '20px' }}>
          <Typography variant="h5">
            hello
          </Typography>
        </Grid>
        <Grid container spacing={2} sx={{ marginTop: '20px' }}>
          <Grid item xs={12} md={6} sx={{ display: 'flex', justifyContent: 'center' }}>
            <img src={Image1} alt="Cute" style={{ maxWidth: '100%', height: 'auto' }} />
          </Grid>
          <Grid item xs={12} md={6}>
            <Grid container direction="column" spacing={2}>
              <Grid item xs={12} sx={{ display: 'flex', justifyContent: 'center' }}>
                <img src={Image2} alt="Second" style={{ maxWidth: '100%', height: 'auto' }} />
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
}