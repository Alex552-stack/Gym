import { useLocaleText } from "@mui/x-date-pickers/internals"
import { useEffect, useState } from "react";
import { Navigate, useLocation, useNavigate } from "react-router-dom"
import agent from "../../api/agent";
import { toast } from "react-toastify";
import LoadinComponent from "../../layout/LoadingComponent";
import { DeleteForeverSharp } from "@mui/icons-material";
import { useDispatch } from "react-redux";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const CheckQr = () => {
    const query = useQuery();
    const qrCode = query.get('code');
    console.log(qrCode);
    const[isValid, setIsValid] = useState(null);
    const [loading,setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const QrCode = useDispatch

    if(qrCode == null)
        navigate('/');

    useEffect(() => {
        /*const checkQrCode =  () => {
            try {
                console.log(qrCode);
                const response = await agent.Visists.ScanQrCode(qrCode);
                if (response.success) {
                    toast.success("QR code registered successfully!");
                    //setIsValid(true);
                } else {
                    throw new Error(response.message || "QR code registration failed");
                }
            } catch (error : any) {
                setError(error.message);
                toast.error(`Error: ${error.message}`);
                //setIsValid(false);
            } finally {
                setLoading(false);
                navigate('/');
            }
        };*/

        const checkQrCode = async () => {
            try{
                agent.Visists.ScanQrCode(qrCode).then((response) => {
                    console.log(response);
                    if(response == null)
                        toast.error("Smth went wrong");
                    else
                        toast.success("Good");
                    navigate('/');
                })
            }
            catch(error : any)
            {
                setError(error.message);
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
    }, [qrCode, navigate]);

    if(loading) return <LoadinComponent/>

    return (
        <></>
    )

}

export default CheckQr;