import { Background } from "@/components/background";
import { AppSidebar } from "@/components/blocks/sidebar/app-sidebar";
import { Separator } from "@/components/ui/separator";
import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from "@/components/ui/sidebar";

export default function DashboardLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <SidebarProvider>
      <AppSidebar />

      <SidebarInset>
        <header className="flex h-14 shrink-0 items-center gap-2">
          <div className="flex flex-1 items-center gap-2 px-3">
            <SidebarTrigger />
            <Separator
              orientation="vertical"
              className="mr-2 data-[orientation=vertical]:h-4"
            />
            Project Management & Task Tracking
          </div>
          <div className="ml-auto px-3"></div>
        </header>
        <Background className="h-full">{children}</Background>
      </SidebarInset>
    </SidebarProvider>
  );
}
