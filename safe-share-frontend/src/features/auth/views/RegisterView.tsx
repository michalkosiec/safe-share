import {type FormEvent, useState} from "react";
import {Link, useNavigate} from "react-router-dom";
import {CheckCircle, Lock, User} from "lucide-react";
import AuthCard from "../componenets/AuthCard.tsx";
import {useAuth} from "../hooks/useAuth.ts";

export default function RegisterView() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const {register} = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError("");

        if (password !== confirmPassword) {
            setError("Passwords do not match");
            return;
        }

        try {
            setIsSubmitting(true);

            // Mocked public key and encrypted private key
            await register(userName, password, "123456", "123456").catch(console.error);
            navigate("/login");
        } catch {
            setError("Cannot register the account");
        } finally {
            setIsSubmitting(false);
        }
    }

    return  (
        <AuthCard title={"Register"}>
            <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6 mt-5">
                <div>
                    <label>User name</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <User />
                        <input
                            type="text"
                            placeholder="Your login"
                            className="w-full bg-transparent outline-none"
                            required
                            disabled={isSubmitting}
                            value={userName}
                            onChange={(e) => {
                                setUserName(e.target.value);
                                if (error) setError("");
                            }}
                        />
                    </div>
                </div>
                <div>
                    <label>Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <Lock />
                        <input
                            type="password"
                            placeholder="Your password"
                            className="w-full bg-transparent outline-none"
                            required
                            disabled={isSubmitting}
                            value={password}
                            onChange={(e) => {
                                setPassword(e.target.value);
                                if (error) setError("");
                            }}
                        />
                    </div>
                </div>
                <div>
                    <label>Confirm Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <CheckCircle />
                        <input
                            type="password"
                            placeholder="Repeat your password"
                            className="w-full bg-transparent outline-none"
                            required
                            disabled={isSubmitting}
                            value={confirmPassword}
                            onChange={(e) => {
                                setConfirmPassword(e.target.value);
                                if (error) setError("");
                            }}
                        />
                    </div>
                </div>
                {error && (
                    <div role="alert" aria-live="polite" className="rounded-md border border-red-400/30 bg-red-500/10 px-3 py-2 text-sm text-red-200">
                        {error}
                    </div>
                )}
                <div>
                    <button
                        type="submit"
                        className="w-full rounded-lg bg-[rgba(227,197,215)] text-black cursor-pointer h-10 mt-5 font-bold hover:bg-[rgba(227,197,215,0.8)] hover:text-black/60 "
                        disabled={isSubmitting || !userName || !password || !confirmPassword}
                    >
                        Enter
                    </button>
                </div>
                <div className="flex justify-center text-sm gap-2">
                    <p>Already have an account?</p><Link to="/login" className="font-bold hover:underline">Log in</Link>
                </div>
            </form>
        </AuthCard>
    )
}