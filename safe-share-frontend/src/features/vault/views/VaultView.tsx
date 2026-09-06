import DashboardCard from "../components/DashboardCard.tsx";
import Sidebar from "../components/Sidebar.tsx";
import FileUploadZone from "../components/FileUploadZone.tsx";

export default function VaultView() {
    return (
        <main className="p-6 pl-0 md:p-5 md:pl-0 h-full w-full overflow-y-auto">
            <div className="grid grid-cols-[280px_1fr] gap-4 h-full">
                <Sidebar />
                <main className="grid grid-cols-12 gap-2 rounded-2xl">
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
                        <FileUploadZone />
                    </DashboardCard>

                    <DashboardCard className="col-span-12 lg:col-span-4">
                        <h2 className="text-xl font-semibold text-white mb-6">Last activity</h2>

                        <div className="space-y-4">
                            <p className="text-sm text-gray-100 italic">No new events</p>
                        </div>
                    </DashboardCard>
                </main>
            </div>
        </main>
    );
}