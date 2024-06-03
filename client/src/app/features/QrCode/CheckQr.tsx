import { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import agent from "../../api/agent";
import { toast } from "react-toastify";
import LoadingComponent from "../../layout/LoadingComponent";
import { useDispatch, useSelector } from "react-redux";
import { scanQrCode } from "../../features/account/accountSlice";
import { RootState, useAppDispatch, useAppSelector } from "../../store/configureStore";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const CheckQr = () => {
    const query = useQuery();
    const qrCode = query.get('code');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const user = useAppSelector(state => state.account);

    useEffect(() => {
        const checkQrCode = async () => {
            if (!qrCode) {
                toast.error("No QR code provided");
                setLoading(false);
                navigate('/');
                return;
            }
            try {
                await dispatch(scanQrCode({ qrCode }));
                if (user) {
                    // User is logged in, scan the QR code
                    await dispatch(scanQrCode({ qrCode }));
                    //toast.success("QR code scanned successfully!");
                } else {
                    // User is not logged in, save the QR code and redirect to login
                    toast.info("You need to log in to scan the QR code");
                    
                    navigate(`/login?redirect=check-qr&code=${qrCode}`);
                }
            } catch (error: any) {
                setError(error.message);
                toast.error(`Error: ${error.message}`);
            } finally {
                setLoading(false);
            }
        };

        if (qrCode) {
            checkQrCode();
        } else {
            toast.error("No QR code provided");
            setLoading(false);
            navigate('/');
        }
    }, [qrCode, navigate, user, dispatch]);

    if (loading) return <LoadingComponent />;

    return <></>;
};

export default CheckQr;
