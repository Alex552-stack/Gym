import { Box, Grid, Typography } from '@mui/material';
import gif from '../../../Images/giphy.gif'; 
 // Adjust the path if necessary
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
    <div
      style={{
        height: '90vh', // Adjust for the height of the navbar
        width: '100%',
        margin: 0,
        padding: 0,
        position: 'relative',
        overflow: 'hidden',
      }}
    >
      <div
        style={{
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundImage: `url(${gif})`,
          backgroundSize: 'cover',
          backgroundRepeat: 'no-repeat',
          zIndex: -1,
        }}
      />
      <Grid
        container
        justifyContent="center"
        alignItems="center"
        sx={{
          height: '100%',
          width: '100%',
          margin: 0,
          padding: 0,
        }}
      >
        <Grid item xs={12} sx={{ height: '100%', position: 'relative' }}>
          <Box
            sx={{
              position: 'absolute',
              top: '50%',
              left: '50%',
              transform: 'translate(-50%, -50%)',
              padding: '20px',
              backgroundColor: 'rgba(0, 0, 0, 0.7)', // 50% transparent black background
              borderRadius: '10px',
            }}
          >
            <Typography
              variant="h3"
              component="div"
              sx={{
                fontSize: isSmallScreen ? '60px' : '100px',
                color: 'transparent',
                backgroundImage: 'linear-gradient(#fff, #fff)',
                backgroundRepeat: 'no-repeat',
                WebkitBackgroundClip: 'text',
                backgroundPosition: '-800px 0',
                animation: 'backcolor 4s linear infinite alternate',
                textAlign: 'center',
                '@keyframes backcolor': {
                  '100%': {
                    backgroundPosition: '0 0',
                  },
                },
              }}
            >
              WELCOME!
            </Typography>
          </Box>
        </Grid>
      </Grid>
    </div>
  );
};
