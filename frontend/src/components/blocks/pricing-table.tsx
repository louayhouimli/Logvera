"use client";

import { useState } from "react";

import { Check, ChevronsUpDown, X } from "lucide-react";

import { Button } from "@/components/ui/button";
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
} from "@/components/ui/collapsible";

interface FeatureSection {
  category: string;
  features: {
    name: string;
    free: true | false | null | string;
    startup: true | false | null | string;
    enterprise: true | false | null | string;
  }[];
}

const pricingPlans = [
  {
    name: "Free",
    button: {
      text: "Get started",
      variant: "outline" as const,
    },
  },
  {
    name: "Startup",
    button: {
      text: "Get started",
      variant: "outline" as const,
    },
  },
  {
    name: "Enterprise",
    button: {
      text: "Get a demo",
      variant: "outline" as const,
    },
  },
];

const comparisonFeatures: FeatureSection[] = [
  {
    category: "Usage",
    features: [
      {
        name: "APIs monitored",
        free: "1",
        startup: "5",
        enterprise: "Unlimited",
      },
      {
        name: "Log events / month",
        free: "50,000",
        startup: "500,000",
        enterprise: "Custom",
      },
      {
        name: "Retention period",
        free: "7 days",
        startup: "30 days",
        enterprise: "90+ days",
      },
      {
        name: "Team members",
        free: "1",
        startup: "5",
        enterprise: "Unlimited",
      },
    ],
  },
  {
    category: "Logs & Monitoring",
    features: [
      {
        name: "Structured log ingestion",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "Advanced filtering & search",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "Performance metrics (latency, error rate)",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "Custom alert rules",
        free: null,
        startup: true,
        enterprise: true,
      },
      {
        name: "Anomaly detection (experimental)",
        free: null,
        startup: null,
        enterprise: true,
      },
    ],
  },
  {
    category: "Platform & Security",
    features: [
      {
        name: "API key authentication",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "JWT-based dashboard access",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "Multiple environments (dev / prod)",
        free: null,
        startup: true,
        enterprise: true,
      },
      {
        name: "Audit logs",
        free: null,
        startup: null,
        enterprise: true,
      },
    ],
  },
  {
    category: "Support",
    features: [
      {
        name: "Community support",
        free: true,
        startup: true,
        enterprise: true,
      },
      {
        name: "Priority support",
        free: null,
        startup: true,
        enterprise: true,
      },
      {
        name: "Dedicated account support",
        free: null,
        startup: null,
        enterprise: true,
      },
    ],
  },
];


const renderFeatureValue = (value: true | false | null | string) => {
  if (value === true) {
    return <Check className="size-5" />;
  }
  if (value === false) {
    return <X className="size-5" />;
  }
  if (value === null) {
    return null;
  }
  // String value
  return (
    <div className="flex items-center gap-2">
      <Check className="size-4" />
      <span className="text-muted-foreground">{value}</span>
    </div>
  );
};

export const PricingTable = () => {
  const [selectedPlan, setSelectedPlan] = useState(1); // Default to Startup plan

  return (
    <section className="pb-28 lg:py-32">
      <div className="container">
        <PlanHeaders
          selectedPlan={selectedPlan}
          onPlanChange={setSelectedPlan}
        />
        <FeatureSections selectedPlan={selectedPlan} />
      </div>
    </section>
  );
};

const PlanHeaders = ({
  selectedPlan,
  onPlanChange,
}: {
  selectedPlan: number;
  onPlanChange: (index: number) => void;
}) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="">
      {/* Mobile View */}
      <div className="md:hidden">
        <Collapsible open={isOpen} onOpenChange={setIsOpen} className="">
          <div className="flex items-center justify-between border-b py-4">
            <CollapsibleTrigger className="flex items-center gap-2">
              <h3 className="text-2xl font-semibold">
                {pricingPlans[selectedPlan].name}
              </h3>
              <ChevronsUpDown
                className={`size-5 transition-transform ${isOpen ? "rotate-180" : ""}`}
              />
            </CollapsibleTrigger>
            <Button
              variant={pricingPlans[selectedPlan].button.variant}
              className="w-fit"
            >
              {pricingPlans[selectedPlan].button.text}
            </Button>
          </div>
          <CollapsibleContent className="flex flex-col space-y-2 p-2">
            {pricingPlans.map(
              (plan, index) =>
                index !== selectedPlan && (
                  <Button
                    size="lg"
                    variant="secondary"
                    key={index}
                    onClick={() => {
                      onPlanChange(index);
                      setIsOpen(false);
                    }}
                  >
                    {plan.name}
                  </Button>
                ),
            )}
          </CollapsibleContent>
        </Collapsible>
      </div>

      {/* Desktop View */}
      <div className="grid grid-cols-4 gap-4 max-md:hidden">
        <div className="col-span-1 max-md:hidden"></div>

        {pricingPlans.map((plan, index) => (
          <div key={index} className="">
            <h3 className="mb-3 text-2xl font-semibold">{plan.name}</h3>
            <Button variant={plan.button.variant} className="">
              {plan.button.text}
            </Button>
          </div>
        ))}
      </div>
    </div>
  );
};

const FeatureSections = ({ selectedPlan }: { selectedPlan: number }) => (
  <>
    {comparisonFeatures.map((section, sectionIndex) => (
      <div key={sectionIndex} className="">
        <div className="border-primary/40 border-b py-4">
          <h3 className="text-lg font-semibold">{section.category}</h3>
        </div>
        {section.features.map((feature, featureIndex) => (
          <div
            key={featureIndex}
            className="text-foreground grid grid-cols-2 font-medium max-md:border-b md:grid-cols-4"
          >
            <span className="inline-flex items-center py-4">
              {feature.name}
            </span>
            {/* Mobile View - Only Selected Plan */}
            <div className="md:hidden">
              <div className="flex items-center gap-1 py-4 md:border-b">
                {renderFeatureValue(
                  [feature.free, feature.startup, feature.enterprise][
                    selectedPlan
                  ],
                )}
              </div>
            </div>
            {/* Desktop View - All Plans */}
            <div className="hidden md:col-span-3 md:grid md:grid-cols-3 md:gap-4">
              {[feature.free, feature.startup, feature.enterprise].map(
                (value, i) => (
                  <div
                    key={i}
                    className="flex items-center gap-1 border-b py-4"
                  >
                    {renderFeatureValue(value)}
                  </div>
                ),
              )}
            </div>
          </div>
        ))}
      </div>
    ))}
  </>
);
