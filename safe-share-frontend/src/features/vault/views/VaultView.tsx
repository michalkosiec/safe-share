import DashboardCard from "../components/DashboardCard.tsx";
import Sidebar from "../components/Sidebar.tsx";

export default function VaultView() {
    return (
        <main className="p-6 md:p-8 h-full w-full overflow-y-auto">
            <div className="grid grid-cols-[300px_1fr] gap-6 h-full">
                <Sidebar />
                <div className="grid grid-cols-12 gap-4 bg-gray-700 rounded-2xl">

                    <header className="col-span-12 mb-2 p-6">
                        <h1 className="text-3xl font-bold text-white">Your Safe</h1>
                        <p className="text-gray-100 mt-1">Manage your encrypted files and access keys</p>
                    </header>

                    <DashboardCard className="col-span-12 md:col-span-4">
                        <h2 className="text-sm font-medium text-gray-100">All of the files</h2>
                        <p className="text-3xl font-bold text-white mt-2">0</p>
                    </DashboardCard>

                    <DashboardCard className="col-span-12 md:col-span-4">
                        <h2 className="text-sm font-medium text-gray-100">Used storage</h2>
                        <p className="text-3xl font-bold text-white mt-2">0 B</p>
                    </DashboardCard>

                    <DashboardCard className="col-span-12 md:col-span-4">
                        <h2 className="text-sm font-medium text-gray-100">Shared keys</h2>
                        <p className="text-3xl font-bold text-white mt-2">0</p>
                    </DashboardCard>

                    <DashboardCard className="col-span-12 lg:col-span-8">
                        <div className="flex justify-between items-center mb-6">
                            <h2 className="text-xl font-semibold text-white">Your files</h2>
                        </div>

                        <div className="flex flex-col items-center justify-center h-64 text-gray-100 border-2 border-dashed border-white/10 rounded-xl">
                            <p>No files yet. Upload something to get started</p>
                        </div>
                    </DashboardCard>

                    <DashboardCard className="col-span-12 lg:col-span-4">
                        <h2 className="text-xl font-semibold text-white mb-6">Last activity</h2>

                        <div className="space-y-4">
                            <p className="text-sm text-gray-100 italic">No new events</p>
                        </div>
                    </DashboardCard>
                </div>
            </div>
        </main>
    );
}