import { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { toast } from "react-toastify";
import LoadingComponent from "../../layout/LoadingComponent";
import { scanQrCode } from "../../features/account/accountSlice";
import {
  useAppDispatch,
  useAppSelector,
} from "../../store/configureStore";

const useQuery = () => {
  return new URLSearchParams(useLocation().search);
};

const CheckQr = () => {
  const query = useQuery();
  const qrCode = query.get("code");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const user = useAppSelector((state) => state.account);

  useEffect(() => {
    const checkQrCode = async () => {
      if (!qrCode) {
        toast.error("No QR code provided");
        setLoading(false);
        navigate("/");
        return;
      }
    if (user) {
        // User is logged in, scan the QR code
        const response = await dispatch(scanQrCode({ qrCode }));
        if ((response.payload as any).success) {
            toast.success("QR code scanned successfully!");
        } else {
            toast.error("Failed to scan QR code");
        }
        setLoading(false);
        navigate('/');
    } else {
        // User is not logged in, save the QR code and redirect to login
        toast.info("You need to log in to scan the QR code");
        navigate(`/login?redirect=check-qr&code=${qrCode}`);
    }
    };

    checkQrCode();
  }, []); // Empty array as dependency

  if (loading) return <LoadingComponent />;

  return <></>;
};

export default CheckQr;
