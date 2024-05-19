import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import { NavLink, useNavigate } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";

//const pages = ["Products", "Pricing", "Blog"];
//const settings = ["Profile", "Account", "Dashboard", "Logout"];

const leftLinks = [
  { title: "Products", path: "/products" },
  { title: "Pricing", path: "/pricing" },
  { title: "Blog", path: "/blog" },
];

const linkesIfConnected = [
  { title: "Account", path: "/account" },
  { title: "LogOut", path: "/logout" },
];

const linksIfNotConnected = [
  { title: "LogIn", path: "/login" },
  { title: "Register", path: "/register" }
]

export default function ResponsiveAppBar() {
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(
    null
  );
  const navigate = useNavigate();

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleClickIfConnected = (title : string) => {
    if(title === "LogOut"){
      localStorage.removeItem('key');
    }
    handleCloseUserMenu;
  } 

  const {user} = useAppSelector(state => state.account);
  const handleCloseNavMenu = (path : string) => {
    setAnchorElNav(null);
    navigate(path);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  return (
    <AppBar position="static" style={{ width: "100vp" }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} 
            component={NavLink}
            to={'/'}
          />
          <Typography
            variant="h6"
            noWrap
            component={NavLink}
            to={'/'}
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            LOGO
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {leftLinks.map((page) => (
                <MenuItem key={page.title} onClick={() => handleCloseNavMenu(page.path)}>
                  <Typography 
                    textAlign="center"
                    style={{ textDecoration: 'none', color: 'inherit'}}         
                  >{page.title}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>
          <AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} 
                  component={NavLink}
                  to={'/'}/>
          <Typography
            variant="h5"
            noWrap
            component={NavLink}
            to={'/'}
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            LOGO
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            {leftLinks.map((page) => (
              <Button
                key={page.title}
                onClick={() => handleCloseNavMenu(page.path)}
                sx={{ my: 2, color: "white", display: "block" }}
              >
                {page.title}
              </Button>
            ))}
          </Box>

          <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: "45px" }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              {
              user? (
              linkesIfConnected.map((setting) => (
                <MenuItem 
                  key={setting.title} 
                  onClick={() => handleClickIfConnected(setting.title)}
                  >
                  <Typography 
                    textAlign="center"
                    style={{color: "inherit",
                    textDecoration: "none"}}
                    component={NavLink}
                    to={setting.path}
                    >{setting.title}</Typography>
                </MenuItem>
              ))):(
                linksIfNotConnected.map((setting) => (
                  <MenuItem 
                    key={setting.title} 
                    onClick={handleCloseUserMenu}
                    >
                    <Typography 
                      textAlign="center"
                      style={{color: "inherit",
                      textDecoration: "none"}}
                      component={NavLink}
                      to={setting.path}
                      >{setting.title}</Typography>
                  </MenuItem>
                )))

            
            }
            </Menu>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
