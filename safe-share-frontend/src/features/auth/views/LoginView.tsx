import {type FormEvent, useState} from "react";
import { User, Lock } from "lucide-react";
import {Link, useNavigate} from "react-router-dom";
import AuthCard from "../componenets/AuthCard.tsx";
import { useAuth } from "../hooks/useAuth.ts";
import {Spinner} from "../../../components/Spinner.tsx";

export default function LoginView() {
    const { login } = useAuth();
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    const navigate = useNavigate();
    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError("");
        setIsSubmitting(true);

        try {
            await login(userName, password);
            navigate("/vault");
        } catch {
            setError("Invalid login credentials");
        } finally {
            setIsSubmitting(false);
        }
    }

    return  (
        <>
            {isSubmitting && <Spinner fullScreen={true} darkOverlay={true} />}
            <AuthCard title={"Log in"}>
                <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6 mt-5">
                    <div>
                        <label>User name</label>
                        <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                            <User />
                            <input type="text" placeholder="Your login" className="w-full bg-transparent outline-none" disabled={isSubmitting} value={userName} onChange={(e) => {
                                setUserName(e.target.value);
                                if (error) setError("");
                            }} />
                        </div>
                    </div>
                    <div>
                        <label>Password</label>
                        <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                            <Lock />
                            <input type="password" placeholder="Your password" className="w-full bg-transparent outline-none" disabled={isSubmitting} value={password} onChange={(e) => {
                                setPassword(e.target.value);
                                if (error) setError("");
                            }} />
                        </div>
                    </div>
                    {error && (
                        <div role="alert" aria-live="polite" className="rounded-md border border-red-400/30 bg-red-500/10 px-3 py-2 text-sm text-red-200">
                            {error}
                        </div>
                    )}
                    <div>
                        <button type="submit" className="w-full rounded-lg bg-[rgba(227,197,215)] text-black cursor-pointer h-10 mt-5 font-bold hover:bg-[rgba(227,197,215,0.8)] hover:text-black/60 ">Enter</button>
                    </div>
                    <div className="flex justify-center">
                        <p>Don't have an account? <Link to="/register" className="font-bold hover:underline">Register</Link></p>
                    </div>
                </form>
            </AuthCard>
        </>
    )
}