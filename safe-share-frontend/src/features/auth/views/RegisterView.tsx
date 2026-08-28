import {type SubmitEvent} from "react";
import {Link} from "react-router-dom";
import {CheckCircle, Lock, User} from "lucide-react";
import AuthCard from "../componenets/AuthCard.tsx";

export default function RegisterView() {

    const handleSubmit = (e: SubmitEvent) => {
        e.preventDefault();
        console.log("Register clicked!")
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
                        />
                    </div>
                </div>
                <div>
                    <button
                        type="submit"
                        className="w-full rounded-lg bg-[rgba(227,197,215)] text-black cursor-pointer h-10 mt-5 font-bold hover:bg-[rgba(227,197,215,0.8)] hover:text-black/60 disabled:opacity-50 disabled:cursor-not-allowed"
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