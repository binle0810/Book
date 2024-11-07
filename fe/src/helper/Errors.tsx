import axios from "axios";
import { toast } from "react-toastify";

export const handleError = (error: any) => {
  if (axios.isAxiosError(error)) {
    var err = error.response;
    if (Array.isArray(err?.data.err)) {
      for (let x of err?.data.errors) {
        toast.warning(x.description);
      }
    } else if (typeof err?.data.errors === "object") {
      for (let x in err?.data.errors) {
        toast.warning(err.data.errors[x][0]);
      }
    } else if (err?.data) {
      toast.warning(err.data);
    } else if (err?.status == 401) {
      toast.warning("Please login");
      window.history.pushState({}, "LoginPage", "/login");
    } else if (err) {
      toast.warning(err?.data);
    }
  }
};